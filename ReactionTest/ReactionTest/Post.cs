using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace ReactionTest
{
    internal class Post : INotifyPropertyChanged
    {
        public string UserID { get; set; }
        public DateTime date { get; set; }
        public int hit { get; set; }
        public int miss { get; set; }
        public int testLength { get; set; }
        public double t1 { get; set; }
        public double t2 { get; set; }
        public double t3 { get; set; }
        public double t4 { get; set; }
        public double t5 { get; set; }
        public double t6 { get; set; }
        public double t7 { get; set; }
        public double t8 { get; set; }
        public double t9 { get; set; }
        public double t10 { get; set; }
        public double t11 { get; set; }
        public double t12 { get; set; }
        public double t13 { get; set; }
        public double t14 { get; set; }
        public double t15 { get; set; }
        public double t16 { get; set; }
        public double t17 { get; set; }
        public double t18 { get; set; }
        public double t19 { get; set; }
        public double t20 { get; set; }
        public double t21 { get; set; }
        public double t22 { get; set; }
        public double t23 { get; set; }
        public double t24 { get; set; }
        public double t25 { get; set; }
        public double t26 { get; set; }
        public double t27 { get; set; }
        public double t28 { get; set; }
        public double t29 { get; set; }
        public double t30 { get; set; }
        public double t31 { get; set; }
        public double t32 { get; set; }
        public double t33 { get; set; }
        public double t34 { get; set; }
        public double t35 { get; set; }
        public double t36 { get; set; }
        public double t37 { get; set; }
        public double t38 { get; set; }
        public double t39 { get; set; }
        public double t40 { get; set; }
        public double t41 { get; set; }
        public double t42 { get; set; }
        public double t43 { get; set; }
        public double t44 { get; set; }
        public double t45 { get; set; }
        public double t46 { get; set; }
        public double t47 { get; set; }
        public double t48 { get; set; }
        public double t49 { get; set; }
        public double t50 { get; set; }
        public double t51 { get; set; }
        public double t52 { get; set; }
        public double t53 { get; set; }
        public double t54 { get; set; }
        public double t55 { get; set; }
        public double t56 { get; set; }
        public double t57 { get; set; }
        public double t58 { get; set; }
        public double t59 { get; set; }
        public double t60 { get; set; }
        public double t61 { get; set; }
        public double t62 { get; set; }
        public double t63 { get; set; }
        public double t64 { get; set; }
        public double t65 { get; set; }
        public double t66 { get; set; }
        public double t67 { get; set; }
        public double t68 { get; set; }
        public double t69 { get; set; }
        public double t70 { get; set; }
        public double t71 { get; set; }
        public double t72 { get; set; }
        public double t73 { get; set; }
        public double t74 { get; set; }
        public double t75 { get; set; }

        public Post(string userID, DateTime date, int hit, int miss, int testLength, double t1, double t2, double t3, double t4, double t5, double t6, double t7, double t8, double t9, double t10, double t11, double t12, double t13, double t14, double t15, double t16, double t17, double t18, double t19, double t20, double t21, double t22, double t23, double t24, double t25, double t26, double t27, double t28, double t29, double t30, double t31, double t32, double t33, double t34, double t35, double t36, double t37, double t38, double t39, double t40, double t41, double t42, double t43, double t44, double t45, double t46, double t47, double t48, double t49, double t50, double t51, double t52, double t53, double t54, double t55, double t56, double t57, double t58, double t59, double t60, double t61, double t62, double t63, double t64, double t65, double t66, double t67, double t68, double t69, double t70, double t71, double t72, double t73, double t74, double t75)
        {
            this.UserID = userID;
            this.date = date;
            this.hit = hit;
            this.miss = miss;
            this.testLength = testLength;
            this.t1 = t1;
            this.t2 = t2;
            this.t3 = t3;
            this.t4 = t4;
            this.t5 = t5;
            this.t6 = t6;
            this.t7 = t7;
            this.t8 = t8;
            this.t9 = t9;
            this.t10 = t10;
            this.t11 = t11;
            this.t12 = t12;
            this.t13 = t13;
            this.t14 = t14;
            this.t15 = t15;
            this.t16 = t16;
            this.t17 = t17;
            this.t18 = t18;
            this.t19 = t19;
            this.t20 = t20;
            this.t21 = t21;
            this.t22 = t22;
            this.t23 = t23;
            this.t24 = t24;
            this.t25 = t25;
            this.t26 = t26;
            this.t27 = t27;
            this.t28 = t28;
            this.t29 = t29;
            this.t30 = t30;
            this.t31 = t31;
            this.t32 = t32;
            this.t33 = t33;
            this.t34 = t34;
            this.t35 = t35;
            this.t36 = t36;
            this.t37 = t37;
            this.t38 = t38;
            this.t39 = t39;
            this.t40 = t40;
            this.t41 = t41;
            this.t42 = t42;
            this.t43 = t43;
            this.t44 = t44;
            this.t45 = t45;
            this.t46 = t46;
            this.t47 = t47;
            this.t48 = t48;
            this.t49 = t49;
            this.t50 = t50;
            this.t51 = t51;
            this.t52 = t52;
            this.t53 = t53;
            this.t54 = t54;
            this.t55 = t55;
            this.t56 = t56;
            this.t57 = t57;
            this.t58 = t58;
            this.t59 = t59;
            this.t60 = t60;
            this.t61 = t61;
            this.t62 = t62;
            this.t63 = t63;
            this.t64 = t64;
            this.t65 = t65;
            this.t66 = t66;
            this.t67 = t67;
            this.t68 = t68;
            this.t69 = t69;
            this.t70 = t70;
            this.t71 = t71;
            this.t72 = t72;
            this.t73 = t73;
            this.t74 = t74;
            this.t75 = t75;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }


}
