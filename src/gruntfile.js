/// <binding AfterBuild='default' />
module.exports = function (grunt) {
    // 1. All configuration goes here 
    grunt.initConfig({
        pkg: grunt.file.readJSON("package.json"),
        copy: {
            pkg: {
                files: [
                    {// copy auth all class and rename them to .pp
                        cwd: "Sample MVC Site/Auth/",
                        expand: true,
                        src: ["**/*.cs", "!obj/**", "!Properties/**"],
                        filter: "isFile",
                        dest: "Lock Stock and Auth/content/Auth/",
                        ext: ".cs.pp"
                    },
                    {// copy all provider class files and rename them to .pp
                        cwd: "Sample MVC Site/Providers/",
                        expand: true,
                        src: ["**/*.cs", "!obj/**", "!Properties/**"],
                        filter: "isFile",
                        dest: "Lock Stock and Auth/content/Providers/",
                        ext: ".cs.pp"
                    },
                    {// copy App_Start/AuthConfig.cs and rename to .pp
                        cwd: "Sample MVC Site/App_Start/",
                        expand: true,
                        src: ["AuthConfig.cs"],
                        filter: "isFile",
                        dest: "Lock Stock and Auth/content/App_Start/",
                        ext: ".cs.pp"
                    },
                    {// copy all view files and rename them to .pp
                        cwd: "Sample MVC Site/Auth/",
                        expand: true,
                        src: ["**/*.cshtml", "!obj/**", "!Properties/**"],
                        filter: "isFile",
                        dest: "Lock Stock and Auth/content/Auth/",
                        ext: ".cshtml.pp"
                    },
                    {// copy view web.config and rename to .pp
                        cwd: "Sample MVC Site/Auth/Views/",
                        expand: true,
                        src: ["web.config"],
                        filter: "isFile",
                        dest: "Lock Stock and Auth/content/Auth/Views/",
                        ext: ".config.pp"
                    },
                    {// copy html client library and rename to .pp
                        cwd: "Sample HTML Client/js/libs/",
                        expand: true,
                        src: ["aspnetAuth.js"],
                        filter: "isFile",
                        dest: "Lock Stock and Auth/content/js/libs/"
                    }
                ]
            },
            bower: {
                files: [
                    {// js bower files
                        cwd: "Lock Stock and Auth/content/js/libs/",
                        expand: true,
                        src: ["**/*.js", "!obj/**", "!Properties/**"],
                        filter: "isFile",
                        dest: "../dist"
                    }
                ]
            }
        },
        replace: {
            pkg: {// do .pp file replacements
                src: ["Lock Stock and Auth/content/**/*.pp"],
                overwrite: true,
                replacements: [
                    {// set $rootnamespace$ to namespace spaces
                        from: /namespace Sample_MVC_Site/g,
                        to: "namespace $rootnamespace$"
                    },
                    {// set $rootnamespace$ to usings
                        from: /using Sample_MVC_Site/g,
                        to: "using $rootnamespace$"
                    },
                    {// set $rootnamespace$ to view models
                        from: /@model Sample_MVC_Site/g,
                        to: "@model $rootnamespace$"
                    },
                    {// set $rootnamespace$ to namespace references in view web.config
                        from: /<add namespace="Sample_MVC_Site"/g,
                        to: "<add namespace=\"$rootnamespace$\""
                    }
                ]
            }
        },
        uglify: {
            options: {
                compress: {
                    drop_console: true
                },
                banner: "/*! Lock stock and auth - v<%= pkg.version %> - " +
                '<%= grunt.template.today("yyyy-mm-dd") %> */ \n' +
                "/*! \n" +
                " * http://www.codehq.co.za \n" +
                " * Copyright 2017 Lock Stock and Auth Authors \n" +
                " * Copyright 2017 Code HQ (Pty) (Ltd) \n" +
                " Licensed under MIT (https://github.com/Halceyon/lock-stock-and-auth/blob/master/LICENSE) \n" +
                " */"
            },
            aspnetAuth: {// minify aspnetAuth.js
                files: {
                    'Lock Stock and Auth/content/js/libs/aspnetAuth.min.js': ["Lock Stock and Auth/content/js/libs/aspnetAuth.js"]
                }
            }
        }
    });

    grunt.loadNpmTasks("grunt-contrib-copy");
    grunt.loadNpmTasks("grunt-text-replace");
    grunt.loadNpmTasks("grunt-contrib-uglify");

    grunt.registerTask("default", ["copy:pkg", "replace:pkg", "uglify", "copy:bower"]);
};