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
        public int t1 { get; set; }
        public int t2 { get; set; }
        public int t3 { get; set; }
        public int t4 { get; set; }
        public int t5 { get; set; }
        public int t6 { get; set; }
        public int t7 { get; set; }
        public int t8 { get; set; }
        public int t9 { get; set; }
        public int t10 { get; set; }
        public int t11 { get; set; }
        public int t12 { get; set; }
        public int t13 { get; set; }
        public int t14 { get; set; }
        public int t15 { get; set; }
        public int t16 { get; set; }
        public int t17 { get; set; }
        public int t18 { get; set; }
        public int t19 { get; set; }
        public int t20 { get; set; }
        public int t21 { get; set; }
        public int t22 { get; set; }
        public int t23 { get; set; }
        public int t24 { get; set; }
        public int t25 { get; set; }
        public int t26 { get; set; }
        public int t27 { get; set; }
        public int t28 { get; set; }
        public int t29 { get; set; }
        public int t30 { get; set; }
        public int t31 { get; set; }
        public int t32 { get; set; }
        public int t33 { get; set; }
        public int t34 { get; set; }
        public int t35 { get; set; }
        public int t36 { get; set; }
        public int t37 { get; set; }
        public int t38 { get; set; }
        public int t39 { get; set; }
        public int t40 { get; set; }
        public int t41 { get; set; }
        public int t42 { get; set; }
        public int t43 { get; set; }
        public int t44 { get; set; }
        public int t45 { get; set; }
        public int t46 { get; set; }
        public int t47 { get; set; }
        public int t48 { get; set; }
        public int t49 { get; set; }
        public int t50 { get; set; }
        public int t51 { get; set; }
        public int t52 { get; set; }
        public int t53 { get; set; }
        public int t54 { get; set; }
        public int t55 { get; set; }
        public int t56 { get; set; }
        public int t57 { get; set; }
        public int t58 { get; set; }
        public int t59 { get; set; }
        public int t60 { get; set; }
        public int t61 { get; set; }
        public int t62 { get; set; }
        public int t63 { get; set; }
        public int t64 { get; set; }
        public int t65 { get; set; }
        public int t66 { get; set; }
        public int t67 { get; set; }
        public int t68 { get; set; }
        public int t69 { get; set; }
        public int t70 { get; set; }
        public int t71 { get; set; }
        public int t72 { get; set; }
        public int t73 { get; set; }
        public int t74 { get; set; }
        public int t75 { get; set; }
        public int t76 { get; set; }
        public int t77 { get; set; }
        public int t78 { get; set; }
        public int t79 { get; set; }
        public int t80 { get; set; }
        public int t81 { get; set; }
        public int t82 { get; set; }
        public int t83 { get; set; }
        public int t84 { get; set; }
        public int t85 { get; set; }
        public int t86 { get; set; }
        public int t87 { get; set; }
        public int t88 { get; set; }
        public int t89 { get; set; }
        public int t90 { get; set; }
        public int t91 { get; set; }
        public int t92 { get; set; }
        public int t93 { get; set; }
        public int t94 { get; set; }
        public int t95 { get; set; }
        public int t96 { get; set; }
        public int t97 { get; set; }
        public int t98 { get; set; }
        public int t99 { get; set; }
        public int t100 { get; set; }

        public Post(string userID, DateTime date, int hit, int miss, int testLength, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14, int t15, int t16, int t17, int t18, int t19, int t20, int t21, int t22, int t23, int t24, int t25, int t26, int t27, int t28, int t29, int t30, int t31, int t32, int t33, int t34, int t35, int t36, int t37, int t38, int t39, int t40, int t41, int t42, int t43, int t44, int t45, int t46, int t47, int t48, int t49, int t50, int t51, int t52, int t53, int t54, int t55, int t56, int t57, int t58, int t59, int t60, int t61, int t62, int t63, int t64, int t65, int t66, int t67, int t68, int t69, int t70, int t71, int t72, int t73, int t74, int t75, int t76, int t77, int t78, int t79, int t80, int t81, int t82, int t83, int t84, int t85, int t86, int t87, int t88, int t89, int t90, int t91, int t92, int t93, int t94, int t95, int t96, int t97, int t98, int t99, int t100)
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
            this.t76 = t76;
            this.t77 = t77;
            this.t78 = t78;
            this.t79 = t79;
            this.t80 = t80;
            this.t81 = t81;
            this.t82 = t82;
            this.t83 = t83;
            this.t84 = t84;
            this.t85 = t85;
            this.t86 = t86;
            this.t87 = t87;
            this.t88 = t88;
            this.t89 = t89;
            this.t90 = t90;
            this.t91 = t91;
            this.t92 = t92;
            this.t93 = t93;
            this.t94 = t94;
            this.t95 = t95;
            this.t96 = t96;
            this.t97 = t97;
            this.t98 = t98;
            this.t99 = t99;
            this.t100 = t100;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }


}
