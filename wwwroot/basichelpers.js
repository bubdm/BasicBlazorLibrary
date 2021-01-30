export function convertRemToPixels(rem) {
    return rem * parseFloat(getComputedStyle(document.documentElement).fontSize);
}
export function getcontainerheight(element) {
    return element.clientHeight;
}
export function getcontainerwidth(element) {
    return element.clientWidth;
}
export function getcontainertop(element) {
    var bodyRect = document.body.getBoundingClientRect();
    var elementRect = element.getBoundingClientRect();
    var offset = elementRect.top - bodyRect.top;
    return offset;
}
export function getcontainerleft(element) {
    var bodyRect = document.body.getBoundingClientRect();
    var elementRect = element.getBoundingClientRect();
    var offset = elementRect.left - bodyRect.left;
    return offset;
}
export function getparentHeight(element) {
    return element.parentElement.clientHeight;
}
export function getparentWidth(element) {
    return element.parentElement.clientWidth;
}