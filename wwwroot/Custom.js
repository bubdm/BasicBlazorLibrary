function copyText() {

    var copyText = document.querySelector("textarea");
    copyText.select();
    copyText.setSelectionRange(0, 99999)
    document.execCommand("copy");
}