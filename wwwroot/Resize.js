import { ResizeListener } from './ResizeModule.js';
let instance = new ResizeListener();
export function listenForResize(dotnetRef, options) {
    instance.listenForResize(dotnetRef, options);
}
export function cancelListener() {
    instance.cancelListener();
}
export function getBrowserWindowSize() {
    return instance.getBrowserWindowSize();
}