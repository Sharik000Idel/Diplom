const form = document.getElementById('search-form');

form.addEventListener('submit', function(event) {
  event.preventDefault();

  const destination = document.getElementById('destination').value;
  const date = document.getElementById('date').value;

  // Send the search query to the server and display the results
  // ...
});
