using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class ShoesController : MonoBehaviour {


    public delegate void OnReceiveHandler(ShoesInfo info);
    public event OnReceiveHandler OnReceive = null;

    public string PortName { get; set; }
    public int BaudRate { get; set; }
    public bool IsRunning { get; private set; }

    private Thread thread;
    private SerialPort serialPort;

    private bool isRecved = false;
    private string recvMessage;



    // Use this for initialization
    void Start()
    {
        this.PortName = "COM3";
        this.BaudRate = 9600;
        this.IsRunning = false;
        this.recvMessage = "";
        this.isRecved = false;
				try {
				File.Delete(@"d:\data.txt");
				}
				catch(Exception) { }

        //Open();

    }

    // Update is called once per frame
    void Update()
    {
		/*
		if (this.IsRunning && this.serialPort != null && this.serialPort.IsOpen)
        {
            try
            {
                if (serialPort.BytesToRead > 0)
                {
                    recvMessage = this.serialPort.ReadLine();
                    isRecved = true;
                }

            }
            catch (Exception e)
            {
            }
        }
		*/
        if (File.Exists(@"d:\data.txt")) {

            if(this.OnReceive != null)
            {

                ShoesInfo info = ShoesInfo.Load();

                if (info != null) {
                    this.OnReceive(info);
                }
				try {
				File.Delete(@"d:\data.txt");
				}
				catch(Exception) { }
            }
        }
    }

    private void OnDestroy()
    {
        Close();
    }

    private void Open()
    {
        this.serialPort = new SerialPort(PortName, BaudRate, Parity.None, 8, StopBits.One);
		//this.serialPort.ReadTimeout= 50000;
        this.serialPort.Open();
		this.serialPort.DiscardInBuffer();
        this.IsRunning = true;

        this.thread = new Thread(Read);
   //     this.thread.Start();
    }

    private void Read()
    {
        while (this.IsRunning && this.serialPort != null && this.serialPort.IsOpen)
        {
            try
            {
                if (serialPort.BytesToRead > 0)
                {
                    recvMessage = this.serialPort.ReadLine();
                    isRecved = true;
                }

            }
            catch (Exception e)
            {
            }
        }
    }

    private void Close()
    {
        IsRunning = false;

        if (IsRunning && thread.IsAlive)
        {
            thread.Join();
        }

        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            serialPort.Dispose();
        }
    }
}
