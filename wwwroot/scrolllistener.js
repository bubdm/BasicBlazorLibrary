export function start(dotnetRef, element) {
    if (element == undefined || element == null) {
        return;
    }
    element.addEventListener('scroll', function () {
        dotnetRef.invokeMethodAsync('ScrollChanged', element.scrollTop);
    });
}