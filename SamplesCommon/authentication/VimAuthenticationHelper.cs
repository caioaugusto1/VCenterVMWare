﻿using System;
using System.IdentityModel.Tokens;
using System.ServiceModel;
using System.ServiceModel.Channels;
using vmware.vim25;

namespace SamplesCommon.authentication
{
    public class VimAuthenticationHelper
    {
        /*
         * Variables of the following types for access to the API methods
         * and to the vSphere inventory.
         * -- ManagedObjectReference for the ServiceInstance on the Server
         * -- VimPortType for access to methods
         * -- ServiceContent for access to managed object services
         */
        public VimPortType VimPortType { get; private set; }
        public ServiceContent ServiceContent { get; private set; }

        private static ManagedObjectReference SVC_INST_REF =
                new ManagedObjectReference();
        public static readonly String VIM_PATH = "/sdk";


        /// <summary>
        /// Creates a session with the server using username and password
        /// </summary>
        /// <param name="server">hostname of the vCenter Server</param>
        /// <param name="username">username for login</param>
        /// <param name="password">password for login</param>
        public void LoginByUsernameAndPassword(
            string server, string username, string password)
        {
            try
            {
                string vimSdkUrl = "https://" + server + VIM_PATH;

                // Obtain a VimPort binding provider
                this.VimPortType = GetVimService(vimSdkUrl, username, password);

                // Retrieve the ServiceContent object and login
                SVC_INST_REF.type = "ServiceInstance";
                SVC_INST_REF.Value = "ServiceInstance";
                ServiceContent =
                    this.VimPortType.RetrieveServiceContent(SVC_INST_REF);

                UserSession userSession = this.VimPortType.Login(
                    ServiceContent.sessionManager, username, password, null);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Logs out of the current session.
        /// </summary>
        public void Logout()
        {
            if (this.VimPortType != null)
            {
                if (this.ServiceContent != null)
                {
                    this.VimPortType.Logout(
                        this.ServiceContent.sessionManager);
                    Console.WriteLine("Logged out successfully.");
                }
                this.VimPortType = null;
                this.ServiceContent = null;
            }
        }

        private VimPortType GetVimService(string url, string username,
            string password)
        {
            var binding = GetCustomBinding();
            var address = new EndpointAddress(url);
            var factory = new ChannelFactory<VimPortType>(binding, address);
            factory.Credentials.UserName.UserName = username;
            factory.Credentials.UserName.Password = password;
            var service = factory.CreateChannel();

            return service;
        }

        public static Binding GetCustomBinding()
        {
            var customBinding = new CustomBinding();

            var security = SecurityBindingElement.CreateUserNameOverTransportBindingElement();
            security.EnableUnsecuredResponse = true;
            security.IncludeTimestamp = true;
            security.AllowInsecureTransport = true;

            var textMessageEncoding = new TextMessageEncodingBindingElement();
            textMessageEncoding.MessageVersion = MessageVersion.Soap11;

            var transport = new HttpsTransportBindingElement

            {
                RequireClientCertificate = false,
                AllowCookies = true,
                MaxReceivedMessageSize = Int32.MaxValue
            };

            customBinding.Elements.Add(security);
            customBinding.Elements.Add(textMessageEncoding);
            customBinding.Elements.Add(transport);

            return customBinding;
        }
        private Binding GetCustomBinding(SecurityKeyType securityKeyType)
        {
            var ws2007FederationHttpBinding =
                GetWS2007FederationHttpBinding(securityKeyType);
            var customBinding = new CustomBinding(
                ws2007FederationHttpBinding.CreateBindingElements());

            foreach (var e in customBinding.Elements)
            {
                if (e is MessageEncodingBindingElement)
                {
                    ((TextMessageEncodingBindingElement)e).MessageVersion =
                        MessageVersion.Soap11;
                }
                if (e is SecurityBindingElement)
                {
                    ((TransportSecurityBindingElement)
                        e).AllowInsecureTransport = true;
                    ((TransportSecurityBindingElement)
                        e).EnableUnsecuredResponse = true;
                    ((TransportSecurityBindingElement)
                        e).IncludeTimestamp = true;
                }
                if (e is HttpsTransportBindingElement)
                {
                    ((HttpsTransportBindingElement)e).AllowCookies = true;
                }
            }
            return customBinding;
        }

        private Binding GetWS2007FederationHttpBinding(
            SecurityKeyType securityKeyType)
        {
            var binding = new WS2007FederationHttpBinding(
                WSFederationHttpSecurityMode.TransportWithMessageCredential);
            binding.Security.Message.NegotiateServiceCredential = false;
            binding.Security.Message.EstablishSecurityContext = false;
            binding.Security.Message.IssuedKeyType = securityKeyType;
            return binding;
        }
    }
}
