var wasrippedoff = true;
export function start(dotnetRef, element) {
    if (element == undefined || element == null) {
        return;
    }
    if (wasrippedoff == true) {
        document.onhelp = new Function("return false;");
        window.onhelp = new Function("return false;");
        document.onkeydown = function (evt) {
            if (evt == null) evt = event; if (testKeyCode(evt, 112)) //F1     
            {
                cancelKeyEvent(evt); //don't allow default help popup by F1
                return false;
            }
            if (evt == null) evt = event; if (testKeyCode(evt, 114)) //F3     
            {
                cancelKeyEvent(evt); //don't allow default search popup by F3
                return false;
            }
        }
        function cancelKeyEvent(evt) {
            if (window.createPopup) evt.keyCode = 0;
            else evt.preventDefault();
        }
        function testKeyCode(evt, intKeyCode) {
            if (window.createPopup) return evt.keyCode == intKeyCode;
            else return evt.which == intKeyCode;
        }
        wasrippedoff = false; //no longer ripped off because now you have full access to f keys including f1 and f3.
    }
    element.addEventListener('keyup', function (evt) {
        dotnetRef.invokeMethodAsync('KeyUp', evt.keyCode);
        if (evt.keyCode != 116 && evt.keyCode != 123) {
            evt.preventDefault();
        }
    });
}