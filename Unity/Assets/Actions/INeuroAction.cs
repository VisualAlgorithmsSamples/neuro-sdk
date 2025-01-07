#nullable enable

using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using NeuroSdk.Websocket;

namespace NeuroSdk.Actions
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public interface INeuroAction
    {
        string Name { get; }

        ActionWindow? ActionWindow { get; }

        /// <summary>
        /// This is ONLY checked when the action is added to an ActionWindow, if it returns false the action won't be added.
        /// </summary>
        sealed bool CanAddToActionWindow(ActionWindow actionWindow) => ActionWindow == null || ReferenceEquals(ActionWindow, actionWindow);

        ExecutionResult Validate(ActionJData actionData, out object? data);
        UniTask ExecuteAsync(object? data);

        WsAction GetWsAction();

        public void SetActionWindow(ActionWindow actionWindow);
    }
}
