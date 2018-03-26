using System.Linq;
using Xunit;
using DeepCopier.Test.TestClasses;
using DeepCopier;
using System.Collections.Generic;

namespace test
{
    public class DeepCopierTest
    {
        /// <summary>
        /// ���Կ���һ��������
        /// </summary>
        [Fact]
        public void TestSelfCopy()
        {
            ClassA a = new ClassA
            {
                ValueTypeProp = 123,
                StringProp = "test"
            };

            ClassA a2 = Copier.Copy(a);

            Assert.Equal(a.ValueTypeProp, a2.ValueTypeProp);
            Assert.Equal(a.StringProp, a2.StringProp);
        }

        /// <summary>
        /// ���Կ����򵥵�����
        /// </summary>
        [Fact]
        public void TestSimpleProperties()
        {
            ClassA a = new ClassA
            {
                ValueTypeProp = 123,
                StringProp = "test"
            };

            ClassB b = Copier.Copy<ClassA, ClassB>(a);

            Assert.Equal(a.ValueTypeProp, b.ValueTypeProp);
            Assert.Equal(a.StringProp, b.StringProp);
        }

        /// <summary>
        /// ���Կ����������͵�����
        /// </summary>
        [Fact]
        public void TestRefTypeProperties()
        {
            ClassB b = new ClassB
            {
                ValueTypeProp = 1,
                StringProp = "string1",
                ClassATypeProp = new ClassA
                {
                    ValueTypeProp = 2,
                    StringProp = "string2",
                }
            };

            ClassC c = Copier.Copy<ClassB, ClassC>(b);

            Assert.Equal(b.ValueTypeProp, c.ValueTypeProp);
            Assert.Equal(b.StringProp, c.StringProp);
            Assert.Equal(b.ClassATypeProp.ValueTypeProp, c.ClassATypeProp.ValueTypeProp);
            Assert.Equal(b.ClassATypeProp.StringProp, c.ClassATypeProp.StringProp);
            Assert.NotSame(b.ClassATypeProp, c.ClassATypeProp);
        }

        /// <summary>
        /// ���Կɱ���������
        /// </summary>
        [Fact]
        public void TestEnumableProperties()
        {
            // ��������Ŀ���
            ClassD d = new ClassD
            {
                VuleTypeArray = new int[] { 1, 2, 3 },
                ClassATypeArray = new ClassA[]
                {
                    new ClassA
                    {
                        ValueTypeProp = 1,
                        StringProp = "string1"
                    },
                    new ClassA
                    {
                        ValueTypeProp = 2,
                        StringProp = "string2"
                    }
                }
            };

            ClassE e = Copier.Copy<ClassD, ClassE>(d);
            Assert.Equal(d.VuleTypeArray, e.VuleTypeArray);
            Assert.NotSame(d.VuleTypeArray, e.VuleTypeArray);

            Assert.Equal(d.ClassATypeArray.Select(x => x.ValueTypeProp),
                e.ClassATypeArray.Select(x => x.ValueTypeProp));

            Assert.Equal(d.ClassATypeArray.Select(x => x.StringProp),
                e.ClassATypeArray.Select(x => x.StringProp));

            Assert.NotEqual(d.ClassATypeArray.AsEnumerable(), e.ClassATypeArray.AsEnumerable());

            Assert.NotSame(d.ClassATypeArray, e.ClassATypeArray);

            // ����List�Ŀ���
            ClassD d2 = new ClassD
            {
                VuleTypeList = new List<int> { 1, 2, 3 },
                ClassATypeList = new List<ClassA>
                {
                    new ClassA
                    {
                        ValueTypeProp = 1,
                        StringProp = "string1"
                    },
                    new ClassA
                    {
                        ValueTypeProp = 2,
                        StringProp = "string2"
                    }
                }
            };

            ClassE e2 = Copier.Copy<ClassD, ClassE>(d2);
            Assert.Equal(d2.VuleTypeList, e2.VuleTypeList);
            Assert.NotSame(d2.VuleTypeList, e2.VuleTypeList);

            Assert.Equal(d2.ClassATypeList.Select(x => x.ValueTypeProp),
                e2.ClassATypeList.Select(x => x.ValueTypeProp));

            Assert.Equal(d2.ClassATypeList.Select(x => x.StringProp),
                e2.ClassATypeList.Select(x => x.StringProp));

            Assert.NotEqual(d2.ClassATypeList.AsEnumerable(), e2.ClassATypeList.AsEnumerable());

            Assert.NotSame(d2.ClassATypeList, e2.ClassATypeList);
        }
    }
}
