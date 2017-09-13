﻿// Papercut
// 
// Copyright © 2008 - 2012 Ken Robertson
// Copyright © 2013 - 2017 Jaben Cargman
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//  
// http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License. 

namespace Papercut.Core.Infrastructure.Container
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using Autofac;
    using Autofac.Core;

    using AutofacSerilogIntegration;

    using Papercut.Common.Domain;
    using Papercut.Core.Domain.Application;
    using Papercut.Core.Domain.Paths;
    using Papercut.Core.Domain.Settings;
    using Papercut.Core.Infrastructure.Logging;
    using Papercut.Core.Infrastructure.MessageBus;
    using Papercut.Core.Infrastructure.Plugins;

    using Serilog;

    using Module = Autofac.Module;

    internal class PapercutCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            new RegisterLogger().Register(builder);
            new RegisterPlugins(Log.Logger).Register(builder, PapercutContainer.ExtensionAssemblies);

            //builder.RegisterAssemblyModules(PapercutContainer.ExtensionAssemblies);

            builder.Register(c => PluginStore.Instance).As<IPluginStore>().SingleInstance();
            builder.RegisterType<PluginReport>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<AutofacServiceProvider>()
                .As<IServiceProvider>()
                .InstancePerLifetimeScope();

            // events
            builder.RegisterType<AutofacMessageBus>()
                .As<IMessageBus>()
                .AsSelf()
                .InstancePerLifetimeScope()
                .PreserveExistingDefaults();

            builder.RegisterType<MessagePathConfigurator>()
                .As<IMessagePathConfigurator>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<JsonSettingStore>()
                .As<ISettingStore>()
                .OnActivated(
                    j =>
                    {
                        try
                        {
                            j.Instance.Load();
                        }
                        catch
                        {
                        }
                    })
                .OnRelease(
                    j =>
                    {
                        try
                        {
                            j.Save();
                        }
                        catch
                        {
                        }
                    })
                .AsSelf()
                .SingleInstance();
        }
    }
}