using System;
using System.Reflection;

public static class ReflectionDeepCopy
{
    public static object Clone(object itemForClone)
    {
        object copyResult;

        if (itemForClone == null)
        {
            Console.WriteLine("source object, needed for copying - is null");
        }

        if (itemForClone.GetType().IsValueType == true)
        {
            copyResult = itemForClone;
        }
        else
        {
            copyResult = Activator.CreateInstance(itemForClone.GetType());
            MemberInfo[] memberCollection = itemForClone.GetType().GetMembers();

            foreach (MemberInfo member in memberCollection)
            {
                if (member.MemberType == MemberTypes.Field)
                {
                    FieldInfo field = (FieldInfo)member;
                    object fieldValue = field.GetValue(itemForClone);

                    if (fieldValue is ICloneable)
                    { 
                        field.SetValue(copyResult, (fieldValue as ICloneable).Clone());
                    }
                    else
                    { 
                        field.SetValue(copyResult, Clone(fieldValue));
                    }
                }
                else if (member.MemberType == MemberTypes.Property)
                { 
                    PropertyInfo myProperty = (PropertyInfo)member;
                    MethodInfo info = myProperty.GetSetMethod(false);

                    if (info != null)
                    {
                        object propertyValue = myProperty.GetValue(itemForClone, null);
                        if (propertyValue is ICloneable)
                        {
                            myProperty.SetValue(copyResult, (propertyValue as ICloneable).Clone(), null);
                        }
                        else
                        {
                            myProperty.SetValue(copyResult, Clone(propertyValue), null);
                        }
                    }
                }
            }
        }
        return copyResult;
    }
}