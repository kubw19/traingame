using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timetableSlide : MonoBehaviour {
    public int imagePosition = 1;
    public Animator animator;
    public void slideDown()
    {
        switch (imagePosition)
        {
            case 0: break;
            case 1: animator.Play("hideTimetable"); imagePosition = 0; break;
            case 2: animator.Play("lowerTimetable"); imagePosition = 1;  break;
        }

    }

    public void slideUp()
    {
        switch (imagePosition)
        {
            case 0: animator.Play("showTimetableMin"); imagePosition = 1; break;
            case 1: animator.Play("showTimetableMax"); imagePosition = 2; break;
            case 2: break;
        }

    }
}
