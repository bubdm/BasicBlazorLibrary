export class ResizeListener {
    throttleResizeHandlerId = -1;
    dotnet;
    resizeHandler = () => {
        this.dotnet.invokeMethodAsync(
            'RaiseOnResized', {
            height: window.innerHeight,
            width: window.innerWidth
        });
    }
    listenForResize(dotnetRef) {
        this.dotnet = dotnetRef;
        window.addEventListener("resize", this.throttleResizeHandler);
        this.resizeHandler();
    }
    throttleResizeHandler = () => {
        clearTimeout(this.throttleResizeHandlerId);
        this.throttleResizeHandlerId = window.setTimeout(this.resizeHandler, 300);
    }
    cancelListener() {
        window.removeEventListener("resize", this.throttleResizeHandler);
    }
    getBrowserWindowSize() {
        return {
            height: window.innerHeight,
            width: window.innerWidth
        };
    }
}