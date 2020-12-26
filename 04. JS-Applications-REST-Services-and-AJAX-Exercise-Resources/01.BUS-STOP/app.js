function getInfo() {
    let inputStopElement = document.getElementById('stopId');
    let stopNameElement = document.getElementById('stopName');
    let busesElement = document.getElementById('buses');

    const stopsArr = ['1287', '1308', '1327', '2334'];

    let url = `https://judgetests.firebaseio.com/businfo/${inputStopElement.value}.json`;

    fetch(url)
        .then(response => handleErrors(response))
        .then(response => response.json())
        .then(data=> {            
            stopNameElement.textContent = data.name;            
            busesElement.innerHTML =  Array.from(Object.keys(data.buses))
                .map(bus=>`<li>Bus ${bus} arrives in ${data.buses[bus]} minutes</li>`)
                .join('');

            inputStopElement.value = '';

        })
        .catch(function(){console.clear()});


        function handleErrors(response) {
            if (!stopsArr.includes(inputStopElement.value)) {
                busesElement.innerHTML = '';
                stopNameElement.textContent = 'ERROR';
                inputStopElement.value = '';
                return;
            }

            if (!response.ok) throw Error(response.statusText);
            return response;
        }
}