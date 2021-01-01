export function scrolltoelement(element) {
    if (element == undefined || element == null) {
        return;
    }
    element.scrollIntoView();
}
export function setscroll(element, pixels) {
    element.scrollTop = pixels;
}