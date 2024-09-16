module.exports = {
    content: [
        './Pages/**/*.{razor,cshtml}',
        './Shared/**/*.{razor,cshtml}',
        './Components/**/*.{razor,cshtml}',
        './wwwroot/**/*.html',
    ],
    theme: {
        extend: {
            colors: {
                customOrange: '#570303', // Turuncu
                customGreen: '#014026',  // Yeþil
            },
        },
    },
    plugins: [],
}
