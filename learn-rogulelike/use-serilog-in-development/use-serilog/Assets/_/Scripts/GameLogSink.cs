using System.Collections;
using System.Collections.Generic;
using System.IO;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using UnityEngine;

public class GameLogSink : ILogEventSink
{
    public GameManager GameManager { get; set; }

    private readonly ITextFormatter _formatter;

    public GameLogSink(ITextFormatter formatter)
    {
        var gcGO = GameObject.FindGameObjectWithTag("GameController");
        if (gcGO != null)
        {
            GameManager = gcGO.GetComponent<GameManager>();
        }

        _formatter = formatter;
    }

    public void Emit(LogEvent logEvent)
    {
        using (var buffer = new StringWriter())
        {
            _formatter.Format(logEvent, buffer);

            if (GameManager != null)
            {
                GameManager.AddLog(buffer.ToString().Trim());
            }
        }
    }
}
