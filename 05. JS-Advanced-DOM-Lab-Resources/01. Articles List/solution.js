function createArticle() {
	let titleInputElement = document.getElementById('createTitle');
	let contentInputElement = document.getElementById('createContent');

	titleInputElement.setAttribute('placeholder','Your title here...')

	contentInputElement.setAttribute('placeholder','Your content here...')

	if (titleInputElement.value!=''&&contentInputElement.value!='') {
		let headingElement = document.createElement('h3');
		headingElement.innerHTML = titleInputElement.value;
		titleInputElement.value = '';

		let contentElement = document.createElement('p');
		contentElement.innerHTML = contentInputElement.value;
		contentInputElement.value = '';

		let articleElement = document.createElement('article');
		articleElement.appendChild(headingElement);
		articleElement.appendChild(contentElement);

		let articleSection = document.getElementById('articles');
		articleSection.appendChild(articleElement);
	}

	
}