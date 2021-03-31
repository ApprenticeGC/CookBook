using System;
using System.Collections;
using System.Collections.Generic;
using Serilog;
// using Serilog.Sinks.Unity3D;
using UnityEngine;

public class TestSerilog : MonoBehaviour
{
    private void Start()
    {
        var log = new LoggerConfiguration()
            // .WriteTo.Unity3D()
            // .WriteTo.GameLog()
            .WriteTo.GameLog()
            .CreateLogger();

        log.Information("Hello Unity");
        log.Information("More from log");
        log.Information("Jack hp: <color=red>hp: 123</color>");
        log.Information("Jack hit <color=blue>Beee</color> for <color=red>2</color> damage");
    }
}
