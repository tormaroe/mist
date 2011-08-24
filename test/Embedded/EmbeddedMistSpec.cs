using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Marosoft.Mist;
using System.Collections;

namespace test.Embedded
{
    [TestFixture]
    public class EmbeddedMistSpec
    {
        private EmbeddedMist mist;

        [SetUp]
        public void Setup()
        {
            mist = new EmbeddedMist();
        }

        //TODO: get and set other variable types (list, any object (waiting for CLI type integration))
        //TODO: call mist function from C#

        [Test]
        public void Set_and_get_string_variable()
        {
            mist.Set("foo", "a string");
            mist.Get<string>("foo").ShouldEqual("a string");
        }

        [Test]
        public void Set_and_get_int_variable()
        {
            mist.Set("foo", 123);
            mist.Get<int>("foo").ShouldEqual(123);
        }

        [Test]
        public void Set_and_get_bool_variable()
        {
            mist.Set("foo", false);
            mist.Get<bool>("foo").ShouldEqual(false);
            mist.Set("bar", true);
            mist.Get<bool>("bar").ShouldEqual(true);
        }

        [Test]
        public void Load_script()
        {
            mist.Load("Embedded\\def_foo_string.mist");
            mist.Get<string>("foo").ShouldEqual("value of foo");
        }

        [Test]
        public void Set_and_call_function__with_sideeffect()
        {
            string side_effect = null;
            mist.Set("foo", () =>
            {
                side_effect = "happened";
                return "value from foo function";
            });
            mist.Load("Embedded\\def_bar_to_value_of_foo_call.mist");
            mist.Get<string>("bar").ShouldEqual("value from foo function");
            side_effect.ShouldEqual("happened");
        }

        [Test]
        public void Set_and_call_function__with_arguments()
        {
            mist.Set<bool, string, IEnumerable<object>, string>(
                "foo", 
                (b, s, l) =>
            {
                b.ShouldBeTrue();
                s.ShouldEqual("two");
                l.Count().ShouldEqual(3);
                l.First().ShouldEqual(1);
                l.Second().ShouldEqual(2);
                l.Third().ShouldEqual(3);
                return "value from foo function";
            });
            mist.Load("Embedded\\def_bar_to_value_of_foo_call_with_args.mist");
            mist.Get<string>("bar").ShouldEqual("value from foo function");            
        }

        [Test]
        public void Call_mist_function()
        {
            mist.Load("Embedded\\def_foo_with_some_args.mist");
            var result = mist.Call<bool, int, List<object>>("foo", false, 12);
            result.Count.ShouldEqual(3);
            result.First().ShouldEqual("args");
            result.Second().ShouldEqual(false);
            result.Third().ShouldEqual(12);
        }
    }
}
