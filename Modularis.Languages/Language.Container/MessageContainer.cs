using System;
using System.Collections.Generic;

namespace Language.Container
{
    public class MessageContainer
    {
        #region Fields

        private static object _instanceLock = new object();

        private object _containerLock = new object();

        private object _directoryLock = new object();

        private string _directory = null;

        private static MessageContainer _instance = null;

        private List<Interface.IMessage> _messages = null;

        #endregion

        #region Properties

        public static MessageContainer Instance
        {
            get 
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new MessageContainer(); 
                    }
                }

                return _instance;
            }
        }

        public List<Interface.IMessage> Messages
        {
            get
            {
                lock (_containerLock)
                {
                    if (_messages == null)
                    {
                        LoadMessages();
                    }

                    return _messages;
                }
            }
        }

        private string Directory
        {
            get
            {
                lock (_directoryLock)
                {
                    if (_directory == null)
                    {
                        _directory = System.Configuration.ConfigurationManager.AppSettings["DirectorioMensajes"];

                        if (string.IsNullOrWhiteSpace(_directory)) 
                        {
                            _directory = AppDomain.CurrentDomain.BaseDirectory;
                        }
                    }
                }

                return _directory;
            }
        }

        #endregion

        #region MessageContainer

        private MessageContainer()
        {
            LoadMessages();
        }

        #endregion

        #region LoadMessages

        private void LoadMessages()
        {
            if (_messages == null) 
            {
                _messages = new List<Interface.IMessage>();
            }
            else 
            {
                _messages.Clear();
            }

            string[] directoryFiles = System.IO.Directory.GetFiles(Directory, "*.dll");

            foreach (string file in directoryFiles)
            {
                var assembly = System.Reflection.Assembly.LoadFile(file);

                if (assembly != null)
                {
                    var types = assembly.GetTypes();

                    foreach (Type type in types)
                    {
                        var interfaces = type.GetInterfaces();

                        foreach (Type interfaceType in interfaces)
                        {
                            if (interfaceType == typeof(Interface.IMessage))
                            {
                                var typeInstance = Activator.CreateInstance(type);

                                if (typeInstance != null)
                                {
                                    _messages.Add(typeInstance as Interface.IMessage);
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
