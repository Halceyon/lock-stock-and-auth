var path = require('path');
var webpack = require('webpack');
var HtmlWebpackPlugin = require('html-webpack-plugin');
var ExtractTextPlugin = require("extract-text-webpack-plugin");
var pluginProposalObjectRestSpread = require('@babel/plugin-proposal-object-rest-spread');

var loaders = [
    {
        test: /\.js$/,
        exclude: /(node_modules|bower_components)/,
        use: {
            loader: 'babel-loader',
            options: {
                presets: ['@babel/preset-env'],
                plugins: [pluginProposalObjectRestSpread]
            }
        }
    },
    {
        test: /\.css$/,
        use: ExtractTextPlugin.extract({
          fallback: "style-loader",
          use: "css-loader"
        })
      }
];

module.exports = {
    node: {        
        fs: 'empty',
        net: 'empty'
    },
    devtool: 'eval-source-map',
    entry: path.resolve(__dirname, 'main.js'),
    output: {
        path: path.resolve('dist'),
        filename: '[name].js',
        publicPath: '/'
    },
    plugins: [
        new HtmlWebpackPlugin({
            template: path.resolve(__dirname, 'index.tpl.html'),
            inject: 'body',
            filename: 'index.html'
        }),        
        new ExtractTextPlugin("styles.css"),
    ],
    module: {
        loaders: loaders
    }
};