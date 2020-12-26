function focus() {
    let divElements = document.querySelectorAll('div div');

    for (const div of divElements) {
        div.addEventListener('mousemove', onMouseMove);
        div.addEventListener('mouseout', onMouseOut);
    }
    function onMouseMove(e) {
        if (e.target.tagName === 'DIV') {
            e.target.classList.add('focused');
        }
        console.log(e.target.className);
    }

    function onMouseOut(e) {
        e.target.classList.remove('focused');
    }
}