function solve() {
    let url = `https://judgetests.firebaseio.com/schedule/`;
    let stopId = 'depot';
    let stopName = 'depot';

    const span = document.getElementsByClassName('info')[0];
    const departBtn = document.getElementById('depart');
    const arriveBtn = document.getElementById('arrive');

    function depart() {
        fetch((url + `${stopId}.json`))
            .then(response => response.json())
            .then(data => {
                stopName = data.name;
                stopId = data.next;

                span.textContent = `Next stop ${data.name}`;
                SwitchButtons();

            })
            .catch(()=>{
                span.textContent = 'Error';
                departBtn.disabled = true;
                arriveBtn.disabled = true;
            })
    }

    function arrive() {
        span.textContent = `Arriving at ${stopName}`;
        SwitchButtons();        
    }


    function SwitchButtons() {
        if (departBtn.disabled === false) {
            departBtn.disabled = true;
            arriveBtn.disabled = false;
        } else {
            departBtn.disabled = false;
            arriveBtn.disabled = true;
        }
    }

    return {
        depart,
        arrive
    };
}

let result = solve();