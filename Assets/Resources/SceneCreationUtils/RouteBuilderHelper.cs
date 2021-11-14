using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Assets.Resources.SceneCreationUtils
{
    public class RouteBuilderHelper
    {
        public static Waypoint From;
        public static Waypoint To;
        public static void AddWaypoint(Waypoint wpt)
        {
            if(From == null)
            {
                From = wpt;
                return;
            }
            if(From == wpt)
            {
                return;
            }
            To = wpt;
        }
        public static void Generate(TracksGenerator generator)
        {
            if (!From.CanGoTo(To) && !To.CanGoTo(From))
            {
                From.AssignWay(To);
                To.AssignWay(From);
            }

            generator.Generate(From, To);
            EditorUtility.SetDirty(From);
            EditorUtility.SetDirty(To);
            Clear();
        }
        public static void Clear()
        {
            From = null;
            To = null;
        }
    }
}
