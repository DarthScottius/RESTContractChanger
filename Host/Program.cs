using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.Title = "Service";


            ServiceHost sh = CreateServiceHost(typeof(Component.Seahawks));

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }


        private static ServiceHost CreateServiceHost(Type component)
        {
            ServiceHost sh = new ServiceHost(component);
            sh.Open();

            Console.WriteLine("Endpoint Listeners:");
            Console.WriteLine("----------------");
            foreach (ChannelDispatcher cd in sh.ChannelDispatchers)
            {
                foreach (EndpointDispatcher epd in cd.Endpoints)
                {
                    Console.Write(epd.EndpointAddress.Uri.AbsoluteUri);

                    ConsoleColor origCol = Console.ForegroundColor;

                    ServiceEndpoint servend = sh.Description.Endpoints.Find(epd.EndpointAddress.Uri);
                    if (servend != null)
                    {
                        Console.WriteLine("\tPhysical listeningURI: " + servend.ListenUri.AbsoluteUri);

                        Console.ForegroundColor = ConsoleColor.DarkYellow;

                        BindingElementCollection bec = servend.Binding.CreateBindingElements();
                        foreach (BindingElement be in bec)
                        {
                            Console.WriteLine("\t" + be.GetType().ToString());
                            SymmetricSecurityBindingElement symBe = be as SymmetricSecurityBindingElement;
                            AsymmetricSecurityBindingElement asymBe = be as AsymmetricSecurityBindingElement;
                            TransportSecurityBindingElement transBe = be as TransportSecurityBindingElement;


                            if (symBe != null)
                            {
                                Console.WriteLine("\t\t" + symBe.ProtectionTokenParameters.GetType().ToString());
                            }

                            if (asymBe != null)
                            {
                                X509SecurityTokenParameters initParms = asymBe.InitiatorTokenParameters as X509SecurityTokenParameters;
                                X509SecurityTokenParameters recParms = asymBe.RecipientTokenParameters as X509SecurityTokenParameters;
                                if (initParms != null)
                                {
                                    Console.WriteLine("\t\tInitiator Security Token Parmeter type");
                                    Console.WriteLine("\t\t\t" + initParms.GetType().ToString() + Environment.NewLine + "\t\t\tInclusionMode: " + initParms.InclusionMode.ToString());
                                }
                                if (recParms != null)
                                {
                                    Console.WriteLine("\t\tRecipient Security Token Parmeter type");

                                    Console.WriteLine("\t\t\t" + recParms.GetType().ToString() + Environment.NewLine + "\t\t\tInclusionMode: " + recParms.InclusionMode.ToString());
                                }
                            }

                            if (transBe != null)
                            {
                                SupportingTokenParameters supTokParms = transBe.EndpointSupportingTokenParameters;

                                if (supTokParms.Endorsing.Count > 0)
                                {
                                    ShowSecurityTokenParmeters(supTokParms.Endorsing, "Endorsing");

                                }
                            }
                        }
                    }


                    Console.ForegroundColor = origCol;
                    Console.WriteLine();
                }

            }
            return sh;
        }


        static void ShowSecurityTokenParmeters(System.Collections.ObjectModel.Collection<SecurityTokenParameters> parmsCollection, string tokenType)
        {
            Console.WriteLine("\t\t" + tokenType + " Tokens");

            foreach (SecurityTokenParameters parms in parmsCollection)
            {
                Console.WriteLine("\t\t\t" + parms.GetType().ToString() + Environment.NewLine + "\t\t\tInclusionMode: " + parms.InclusionMode.ToString());
                SecureConversationSecurityTokenParameters secureConvTokenParms = parms as SecureConversationSecurityTokenParameters;
                if (secureConvTokenParms != null)
                {
                    ShowSecurityTokenParmeters(secureConvTokenParms.BootstrapSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing, "Bootstrap Endorsing");
                }
            }
            
        }
    }
}
