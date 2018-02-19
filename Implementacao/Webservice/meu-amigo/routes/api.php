<?php

use Illuminate\Http\Request;
use App\Curso;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
*/

Route::middleware('auth:api')->get('/user', function (Request $request) {
    return $request->user();
});

Route::get('cursos', 'CursoController@index');
Route::get('interesses', 'InteresseController@index');
Route::get('linguas', 'LinguaController@index');