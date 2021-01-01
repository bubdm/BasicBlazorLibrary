export function convertRemToPixels(rem) {
    return rem * parseFloat(getComputedStyle(document.documentElement).fontSize);
}
export function getcontainerheight(element) {
    return element.clientHeight;
}