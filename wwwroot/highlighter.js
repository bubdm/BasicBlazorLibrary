export function highlighttext(element, startat) {
    element.setSelectionRange(startat, 100000);
    element.focus(); //this time, use the javascript focus method.
}