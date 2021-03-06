﻿using log4net;
using MiNET;
using MiNET.Utils;
using Topshelf;

namespace LeetProxy.Node
{
	public class LeetNodeService
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(LeetNodeService));
		private MiNetServer _server;

		private void Start()
		{
			Log.Info("Starting LeetNode");

			_server = new MiNetServer
			{
				ServerRole = ServerRole.Node
			};

			_server.ServerManager = new NodeServerManager(_server, Config.GetProperty("port", 19132));
			_server.LevelManager = new SpreadLevelManager(10);

			_server.StartServer();
		}

		private void Stop()
		{
			Log.Info("Stopping LeetNode");

			_server.StopServer();
		}

		private static void Main()
		{
			HostFactory.Run(host =>
			{
				host.Service<LeetNodeService>(s =>
				{
					s.ConstructUsing(construct => new LeetNodeService());
					s.WhenStarted(service => service.Start());
					s.WhenStopped(service => service.Stop());
				});

				host.RunAsLocalService();
				host.SetDisplayName("LeetNode Service");
				host.SetDescription("LeetNode MiNET Node Service");
				host.SetServiceName("LeetNode");
			});
		}
	}
}
