//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


//namespace _10_3_Figures.undoredo
//{
//    class Create : Action
//    {
//        public Shape point;

//        public Create(Shape point1)
//        {
//            point = point1;

//        }
//        public override void Undo()
//        {
//            for (int i = 0; i < shapes.Count; i++)
//            {
//                if (Shape.allPoints[i].index == point.index)
//                {
//                    Shape.allPoints.RemoveAt(i);
//                }
//            }
//        }
//        public override void Redo()
//        {
//            Shape.allPoints.Add(point);
//        }
//    }
//}
