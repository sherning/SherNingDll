using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class BuilderConcept
    {
        public static void Main()
        {
            // Understanding the concept of builder concept.
            // Why return this.Because subsequent functions cannot call if return is anything but the object.
            // As instance methods can only be called on instances until static extension methods.
            HTMLBuilder builder = new HTMLBuilder("ul");
            builder
                .AddChild("li", "Hello")
                .AddChild("li","World");

            builder.AddChild("li", "Fluent Call Approach");
            Console.WriteLine(builder);
        }

        private class HTMLBuilder
        {
            private readonly string RootName;
            private HTMLElement Root;
            public HTMLBuilder(string rootName)
            {
                // Use the constructor to initialize this obect 
                Root = new HTMLElement();
                Root.Name = rootName;

                // Save this inside HTML Builder.
                RootName = rootName;
            }

            public HTMLBuilder AddChild(string childName, string childText)
            {
                HTMLElement element = new HTMLElement(childName, childText);

                // Accessing the elements inside HTML Element.
                // Root.Name = ul, Root object serves mainly to store the list of elements.
                Root.Elements.Add(element);

                // Fluent interface. Chain several calls
                // in this demo, "this" -> builder.
                return this;
            }
            public override string ToString()
            {
                return Root.ToString();
            }
            public void Clear()
            {
                // Root = new HTMLELement{Name = Root};
                Root = new HTMLElement();
                Root.Name = RootName;
            }
        }

        private class HTMLElement
        {
            public string Name { get; set; }
            public string Text { get; set; }

            // A list of HTML elements
            public List<HTMLElement> Elements = new List<HTMLElement>();

            // works with only readonly, and not const.
            private readonly int IndentSize;

            // Default constructor
            public HTMLElement()
            {

            }

            // Set indentSize = 2, if left empty during initialization, it defaults to 2.
            public HTMLElement(string name, string text, int indentSize = 2)
            {
                Name = name;
                Text = text;
                IndentSize = indentSize;
            }
            private string ToStringImpl(int indent)
            {
                StringBuilder sb = new StringBuilder();

                // Indent size by default is 2
                string str = new string(' ', IndentSize * indent);

                // No spaces for this Append.
                sb.AppendLine($"{str}<{Name}>");

                // Check if text is empty, if NOT empty then ...
                if (string.IsNullOrWhiteSpace(Text) == false)
                {
                    // This by default will translate to two spaces.
                    sb.Append(new string(' ', IndentSize * (indent + 1)));

                    // This will print add to end of current append, and break a new line.
                    sb.AppendLine(Text);
                }

                foreach (var element in Elements)
                {
                    // For each element, we add to string builder this format.
                    sb.Append(element.ToStringImpl(indent + 1));
                }

                sb.AppendLine($"{str}</{Name}>");

                return sb.ToString();
            }

            public override string ToString()
            {
                return ToStringImpl(0);
            }
        }

    }
}
