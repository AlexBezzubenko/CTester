using System.Runtime.Serialization;
using System;
using System.Security.Permissions;

namespace TestApp.Model
{
    [Serializable]
    public class Answer: ISerializable
    {       
        public bool Correct { get; set; }

        public string Text { get; set; }

        public int Number { get; set; }

        public Answer()
        {
            Text = "---";
        }
        public Answer(string text)
        {
            this.Text = text;
        }
        public Answer(string text, bool Correct)
        {
            this.Text = text;
            this.Correct = Correct;
        }

        public override string ToString()
        {
            return Text;
        }
        private Answer( SerializationInfo info, StreamingContext ctxt )
        {   
            this.Text = info.GetString("Text");
            this.Correct = info.GetBoolean("Correct");
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData( SerializationInfo info, StreamingContext ctxt )
        {
           info.AddValue("Text", this.Text);
           info.AddValue("Correct", this.Correct);
        }
    }
}