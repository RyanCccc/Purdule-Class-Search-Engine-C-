using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TYPE {
    enum TYPE { 
        Distance=0,Lecture,Lab,Recitation
    };
}
namespace SeatRemain
{
    class Course
    {
        String courseNum;
        List<Class>[] course=new List<Class>[4];
         
        public Course(String courseNum) {
            this.courseNum = courseNum;
        }
        public void AddClass(Class c,TYPE.TYPE T) {
            course[(int)T].Add(c);
        }
        public void deleteClass(Class c, TYPE.TYPE T) {
            course[(int)T].Remove(c);
        }

    }
}
