export function start(dotnetRef, element) {
	if (element == undefined || element == null) {
		return;
	}
	var isScrolling;
	element.addEventListener('scroll', function () {

		dotnetRef.invokeMethodAsync('ScrollChanged', element.scrollTop);

		//clearTimeout(isScrolling);
		//isScrolling = setTimeout(function () {

		//	dotnetRef.invokeMethodAsync('ScrollChanged', element.scrollTop);
		//}, 66);
	}, false);
}