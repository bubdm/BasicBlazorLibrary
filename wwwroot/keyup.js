export function start(dotnetRef, element) {
    if (element == undefined || element == null) {
        return;
    }
    element.addEventListener('keyup', function (evt) {
        dotnetRef.invokeMethodAsync('KeyUp', evt.keyCode);
        if (evt.keyCode != 116 && evt.keyCode != 123) {
            evt.preventDefault();
        }
    });
}