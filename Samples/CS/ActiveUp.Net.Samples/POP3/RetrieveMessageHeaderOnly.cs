using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.POP3
{
    public partial class RetrieveMessageHeaderOnly : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public RetrieveMessageHeaderOnly(SamplesConfiguration config)
        {
            this.Config = config;
            InitializeComponent();
            InitializeSample();
        }

        protected override void InitializeSample()
        {
            base.InitializeSample();

            _tbUserName.Text = Config.Pop3UserName;
            _tbPassword.Text = Config.Pop3Password;
            _tbPop3Server.Text = Config.Pop3Server;
        }

        private void _bCountMessage_Click(object sender, EventArgs e)
        {
            // We instantiate the pop3 client.
            Pop3Client pop = new Pop3Client();

            try
            {
                this.AddLogEntry(string.Format("Connection to the pop 3 server : {0}", _tbPop3Server.Text));

                // Connect to the pop3 client
                pop.Connect(_tbPop3Server.Text, _tbUserName.Text, _tbPassword.Text);

                //Retrive the header of the messag at position 1
                if (pop.MessageCount > 0)
                {
                    Header msgHeader = pop.RetrieveHeaderObject(1);

                    //Display the header for the first message
                    this.AddLogEntry(string.Format("Subject: {0} From :{1} Date Sent {2}"
                                        , msgHeader.Subject, msgHeader.From.Email, msgHeader.DateString));
                }

                else
                {
                    this.AddLogEntry("There is no message in this pop3 account");
                }
            }

            catch (Pop3Exception pexp)
            {
                this.AddLogEntry(string.Format("Pop3 Error: {0}", pexp.Message));
            }

            catch (Exception ex)
            {
                this.AddLogEntry(string.Format("Failed: {0}", ex.Message));
            }

            finally
            {
                if (pop.IsConnected)
                {
                    pop.Disconnect();
                }
            }
        }
    }
}

