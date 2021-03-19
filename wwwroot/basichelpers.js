﻿export function convertRemToPixels(rem) {
    return rem * parseFloat(getComputedStyle(document.documentElement).fontSize);
}
export function getcontainerheight(element) {
    if (element == null) {
        return 0;
    }
    return element.clientHeight;
}
export function getcontainerwidth(element) {
    if (element == null) {
        return 0;
    }
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
    if (element == null) {
        return 0;
    }
    return element.parentElement.clientHeight;
}
export function getparentWidth(element) {
    //return 0;
    if (element == null) {
        return 0;
    }
    return element.parentElement.clientWidth;
}