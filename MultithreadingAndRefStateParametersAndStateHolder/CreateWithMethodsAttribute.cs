using System;

namespace MultithreadingAndRefStateParametersAndStateHolder
{
    public class CreateWithMethodsAttribute : Attribute
    {
        public Type[] Types { get; }

        public CreateWithMethodsAttribute(params Type[] types)
        {
            Types = types;
        }
    }
}