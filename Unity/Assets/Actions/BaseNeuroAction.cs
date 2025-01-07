#nullable enable

using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using NeuroSdk.Json;
using NeuroSdk.Websocket;
using UnityEngine;

namespace NeuroSdk.Actions
{
    [PublicAPI]
    public abstract class BaseNeuroAction : INeuroAction
    {
        /// <summary>
        /// The value that was passed to the actionWindow parameter in the constructor
        /// </summary>
        protected ActionWindow? ActionWindow;

        protected BaseNeuroAction() : this(null) { }

        protected BaseNeuroAction(ActionWindow? actionWindow)
        {
            ActionWindow = actionWindow;
        }

        public abstract string Name { get; }
        protected abstract string Description { get; }
        protected abstract JsonSchema? Schema { get; }

        public virtual bool CanAddToActionWindow(ActionWindow actionWindow) => ActionWindow == null || ReferenceEquals(ActionWindow, actionWindow);

        ExecutionResult INeuroAction.Validate(ActionJData actionData, out object? parsedData)
        {
            ExecutionResult result = Validate(actionData, out parsedData);

            if (ActionWindow != null)
            {
                return ActionWindow.Result(result);
            }

            return result;
        }

        UniTask INeuroAction.ExecuteAsync(object? data) => ExecuteAsync(data);

        public virtual WsAction GetWsAction()
        {
            return new WsAction(Name, Description, Schema);
        }

        protected abstract ExecutionResult Validate(ActionJData actionData, out object? parsedData);
        protected abstract UniTask ExecuteAsync(object? data);

        public void SetActionWindow(ActionWindow actionWindow)
        {
            if (!CanAddToActionWindow(actionWindow))
            {
                Debug.LogError("Cannot set the action window for this action.");
                return;
            }
            ActionWindow = actionWindow;
        }
    }
}
