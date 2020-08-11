const path = require('path');
const webpack = require('webpack');
const TerserPlugin = require('terser-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const OptimizeCSSAssetsPlugin = require('optimize-css-assets-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = (env, argv) => {
    return {
        entry: [path.resolve(__dirname, 'Web/index.ts')],
        output: {
            path: path.resolve(__dirname, 'wwwroot/dist'),
            filename: 'bundle.js',
            publicPath: '/dist'
        },
        mode: argv.mode,
        module: {
            rules: [
                {
                    test: /\.tsx?$/,
                    use: [
                        {
                            loader: 'awesome-typescript-loader',
                            options: {
                                configFile: path.join(__dirname, '/tsconfig.webpack.json'),
                                silent: true,
                            }
                        }
                    ]
                },
                {
                    test: /\.html$/,
                    loader: 'html-loader',
                    options: {
                        minimize: true,
                        removeComments: true,
                        collapseWhitespace: true,
                    },
                },
                {
                    test: /\.(sa|sc)ss$/,
                    use: [MiniCssExtractPlugin.loader, 'css-loader', 'sass-loader'],
                },
                {
                    test: /\.(jpe?g|png|gif)$/,
                    loader: 'file-loader',
                    options: {
                        outputPath: 'assets/',
                        publicPath: '/dist/assets'
                    },
                },
                {
                    test: /\.(eot|svg|ttf|woff2?|otf)$/,
                    loader: 'file-loader',
                    options: {
                        outputPath: 'assets/',
                        publicPath: '/dist/assets'
                    },
                },
            ],
        },
        plugins: [
            new webpack.ProvidePlugin({
                $: 'jquery',
                jQuery: 'jquery',
                'window.$': 'jquery',
                'window.jQuery': 'jquery',
                Waves: 'node-waves',
                _: 'underscore',
                Promise: 'es6-promise',
            }),
            new MiniCssExtractPlugin({
                filename: "styles.css",
                cssProcessorOptions: {
                    safe: true,
                    discardComments: {
                        removeAll: true,
                    },
                },
            })
        ],
        resolve: {
            extensions: ['.ts', '.tsx', '.js'],
            modules: ['node_modules'],
        },
        optimization: {
            minimizer: [
                new TerserPlugin({
                    parallel: true,
                    sourceMap: true,
                    terserOptions: {
                        output: {
                            comments: false,
                        },
                    },
                }),
                new OptimizeCSSAssetsPlugin({}),
            ],
        },
        performance: {
            hints: false,
        },
    };
};
