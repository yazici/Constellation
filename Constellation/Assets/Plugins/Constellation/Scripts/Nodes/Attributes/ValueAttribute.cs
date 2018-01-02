﻿namespace Constellation.Attributes {
    public class ValueAttribute : INode, IReceiver, IAwakable, IAttribute{
        private ISender sender;
        private Variable Value;
        private float previousValue;
        public const string NAME = "ValueAttribute";

        public void Setup (INodeParameters _node, ILogger _logger) {
            sender = _node.AddOutput (true, "Current value");
            var newValue = new Variable (0);
            var nameValue = new Variable ("AttributeName");
            Value = newValue;
            _node.AddAttribute (nameValue, Attribute.AttributeType.Word, "Attribute name");

        }

        public string NodeName () {
            return NAME;
        }

        public string NodeNamespace () {
            return NameSpace.NAME;
        }

        public void SetAttribute (Variable var) {
            Value.Set (var.GetFloat ());
        }

        public void OnAwake () {
            previousValue = Value.GetFloat();
            sender.Send (Value, 0);
        }

        public void Receive (Variable _value, Input _input) {
            if (_value.IsFloat ()) {
              Value.Set (_value.GetFloat ());
            }
            if (_input.isWarm)
                sender.Send (Value, 0);
        }
    }
}