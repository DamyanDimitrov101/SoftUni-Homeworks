function lockedProfile() {
    let profiles = Array.from(document.getElementsByClassName('profile'));


    profiles.forEach(x => x.addEventListener('click', onShowMore));

    function onShowMore(e) {
        if (e.target.tagName === 'BUTTON') {
            let button = e.target;

            let inputs = [...e.target.parentElement.children].filter(x => x.checked === true)
                .filter(x => x.value === 'unlock');

            if (inputs.length > 0) {

                let divSToShow = button.parentElement.lastElementChild.previousElementSibling;
                divSToShow.style.display = divSToShow.style.display === 'block' ?
                                                                    'none' : 'block';
                button.innerHTML = button.innerHTML === 'Hide it' ?
                                                                'Show more' : 'Hide it';
                
            }
        }
    }

}