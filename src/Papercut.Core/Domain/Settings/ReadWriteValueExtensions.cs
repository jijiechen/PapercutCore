// Papercut
// 
// Copyright � 2008 - 2012 Ken Robertson
// Copyright � 2013 - 2017 Jaben Cargman
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

namespace Papercut.Core.Domain.Settings
{
    using System;

    using Papercut.Common.Extensions;
    using Papercut.Core.Annotations;

    public static class ReadWriteValueExtensions
    {
        public static void Set(
            [NotNull] this IWriteValue<string> writeValue,
            [NotNull] string key, object value)
        {
            if (writeValue == null) throw new ArgumentNullException(nameof(writeValue));
            if (key == null) throw new ArgumentNullException(nameof(key));

            writeValue.Set(key, value.ToType<string>());
        }

        public static T Get<T>(
            [NotNull] this IReadValue<string> readValue,
            [NotNull] string key,
            [NotNull] Func<T> getDefaultValue)
        {
            if (readValue == null) throw new ArgumentNullException(nameof(readValue));
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (getDefaultValue == null) throw new ArgumentNullException(nameof(getDefaultValue));

            string value = readValue.Get(key);
            return value.IsDefault() ? getDefaultValue() : value.ToType<T>();
        }

        public static T Get<T>(
            [NotNull] this IReadValue<string> readValue,
            [NotNull] string key,
            [CanBeNull] T defaultValue = default(T))
        {
            if (readValue == null) throw new ArgumentNullException(nameof(readValue));
            if (key == null) throw new ArgumentNullException(nameof(key));

            string value = readValue.Get(key);
            return value.IsDefault() ? defaultValue : value.ToType<T>();
        }
    }
}