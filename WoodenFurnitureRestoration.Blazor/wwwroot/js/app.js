console.log('✅ app.js loaded');

window.getInputValue = function (elementId) {
    const element = document.getElementById(elementId);
    const value = element ? element.value : '';
    console.log(`getInputValue('${elementId}') = '${value}'`);
    return value;
};

window.setLocalStorage = function (key, value) {
    localStorage.setItem(key, value);
    console.log(`setLocalStorage('${key}', '${value}') done`);
};

window.getLocalStorage = function (key) {
    const value = localStorage.getItem(key);
    console.log(`getLocalStorage('${key}') = '${value}'`);
    return value;
};