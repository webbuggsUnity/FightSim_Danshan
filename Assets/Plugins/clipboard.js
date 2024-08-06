function GetClipboardText() {
    navigator.clipboard.readText()
        .then(text => {
            // Call the Unity method to set the clipboard text
            window.unityInstance.SendMessage('GameObjectName', 'SetClipboardText', text);
        })
        .catch(err => {
            console.error('Failed to read clipboard contents: ', err);
        });
}