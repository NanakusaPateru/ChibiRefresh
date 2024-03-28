using Cysharp.Threading.Tasks;
using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Manager
{
	public enum TokenType
	{
		SceneChange = 0,
		GameEnd
	}

	/// <summary>
	/// CancellationTokenÇä«óù
	/// </summary>
	public class CancellationTokenManager : SingletonManagerBase<CancellationTokenManager>
	{
		private Dictionary<TokenType, CancellationTokenSource> currentTokenSources = new Dictionary<TokenType, CancellationTokenSource>();

		public override void Initialize()
		{
			base.Initialize();
			currentTokenSources = new Dictionary<TokenType, CancellationTokenSource>();

			foreach(TokenType tokenType in Enum.GetValues(typeof(TokenType)))
			{
				currentTokenSources.Add(tokenType, new CancellationTokenSource());
			}
		}

		/// <summary>
		/// äeéÌUniTaskÇåƒÇ—èoÇ∑ç€ÇÃà¯êîÇ…ë„ì¸Ç∑ÇÈToken
		/// </summary>
		/// <param name="tokenType"></param>
		/// <returns></returns>
		public CancellationToken GetToken(TokenType tokenType)
		{
			if (currentTokenSources.ContainsKey(tokenType))
			{
				return currentTokenSources[tokenType].Token;
			}

			currentTokenSources.Add(tokenType, new CancellationTokenSource());
			return currentTokenSources[tokenType].Token;
		}

		public CancellationToken GetObjectDestoryToken(GameObject gameObject)
		{
			return gameObject.GetCancellationTokenOnDestroy();
		}

		public void DisposeToken(TokenType tokenType)
		{
			if (!currentTokenSources.ContainsKey(tokenType))
			{
				return;
			}

			var tokenSource = currentTokenSources[tokenType];

			if (tokenSource != null)
			{
				tokenSource.Cancel();
				tokenSource.Dispose();

				currentTokenSources[tokenType] = new CancellationTokenSource();
			}
		}
	}
}