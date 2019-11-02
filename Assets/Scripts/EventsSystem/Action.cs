using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Action
{
    public string actionMessage { get; set; }
    public KeyCode[] actionCodes { get; set; }
    public ActionResult actionResult { get; set; }
    public string messageToSuccess { get; set; }
}

