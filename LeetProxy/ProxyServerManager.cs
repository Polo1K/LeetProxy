﻿using System.Collections.Generic;
using System.Net;
using MiNET;

namespace LeetProxy.Server
{
	public class ProxyServerManager : IServerManager
	{
		private readonly List<ProxyNodeConnection> _serverConnections = new List<ProxyNodeConnection>();

		public ProxyServerManager()
		{
			_serverConnections.Add(new ProxyNodeConnection(new IPEndPoint(IPAddress.Loopback, 19133)));
		}

		public IServer GetServer()
		{
			return _serverConnections[0];
		}
	}
}