module.exports = function (grunt) {

    grunt.initConfig({
        bowercopy: {
            options: {
                clean: false
            },
            libs: {
                options: {
                    destPrefix: 'js/libs'
                },
                files: {
                    'jquery.js': 'jquery/dist/jquery.js',
                    'bootstrap.js': 'bootstrap/dist/js/bootstrap.min.js'
                }
            },
            css: {
                files: {
                    'css': ['bootstrap/dist/css', 'font-awesome/css'],
                }
            },
            fonts: {
                files: {
                    'fonts': ['bootstrap/dist/fonts', 'font-awesome/fonts']
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-bowercopy');
    grunt.registerTask('default', ['bowercopy']);

};