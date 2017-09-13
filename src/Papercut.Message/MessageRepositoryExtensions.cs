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

namespace Papercut.Message
{
    using System;

    using Papercut.Core.Domain.Message;

    public static class MessageRepositoryExtensions
    {
        public static byte[] GetMessage(
            this MessageRepository messageRepository,
            MessageEntry entry)
        {
            if (messageRepository == null) throw new ArgumentNullException(nameof(messageRepository));
            if (entry == null) throw new ArgumentNullException(nameof(entry));

            return messageRepository.GetMessage(entry.File);
        }
    }
}