﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SimpleApi.WpfClient {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SimpleApi.WpfClient.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Автоматическа отправка ранее не отрпавленых сообщений завершилась неудачно.
        /// </summary>
        internal static string AutoSendFail {
            get {
                return ResourceManager.GetString("AutoSendFail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Повторная попытка отправки будет произведена позднее автоматически..
        /// </summary>
        internal static string AutoSendOn {
            get {
                return ResourceManager.GetString("AutoSendOn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Автоматическа отправка ранее не отрпавленых сообщений завершилась частично успешно..
        /// </summary>
        internal static string AutoSendPartially {
            get {
                return ResourceManager.GetString("AutoSendPartially", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Автоматическа отправка ранее не отрпавленых сообщений завершилась успешно..
        /// </summary>
        internal static string AutoSendSuccess {
            get {
                return ResourceManager.GetString("AutoSendSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ошибка сохранения сообщения в локальную БД! Сообщение не отправлено!.
        /// </summary>
        internal static string DbErrorAddNote {
            get {
                return ResourceManager.GetString("DbErrorAddNote", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ошибка сохранения результата отправки в локальную БД!.
        /// </summary>
        internal static string DbErrorAddSending {
            get {
                return ResourceManager.GetString("DbErrorAddSending", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ошибка базы данных! Не удалось считать неотрплавнные сообщения!.
        /// </summary>
        internal static string DbErrorGetNotSendedMessages {
            get {
                return ResourceManager.GetString("DbErrorGetNotSendedMessages", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Имеются неотправленные сообщения с прошлых сессий.
        /// </summary>
        internal static string HaveNotSendedMessage {
            get {
                return ResourceManager.GetString("HaveNotSendedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Неотправленных сообщений не имеется..
        /// </summary>
        internal static string NoNotSendedMessage {
            get {
                return ResourceManager.GetString("NoNotSendedMessage", resourceCulture);
            }
        }
    }
}
