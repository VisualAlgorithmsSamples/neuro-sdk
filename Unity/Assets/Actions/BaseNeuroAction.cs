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
        /// The value that was set by the <see cref="SetActionWindow"/> method.
        /// </summary>
        public ActionWindow? ActionWindow { get; private set; } // getter is from INeuroAction

        protected BaseNeuroAction()
        {
            ActionWindow = null;
        }

        [System.Obsolete("This way of setting the action window is obsolete. Please use the parameterless constructor instead.")]
        protected BaseNeuroAction(ActionWindow? actionWindow)
        {
            ActionWindow = actionWindow;
        }

        public abstract string Name { get; }
        protected abstract string Description { get; }
        protected abstract JsonSchema? Schema { get; }

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
            if (!((INeuroAction) this).CanAddToActionWindow(actionWindow))
            {
                Debug.LogError("Cannot set the action window for this action.");
                return;
            }
            ActionWindow = actionWindow;
        }
    }
}
