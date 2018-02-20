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
Route::get('login', 'UsuarioController@login');
Route::post('usuarios', 'UsuarioController@store');
Route::post('interesses', 'InteresseController@store');
Route::post('linguas', 'LinguaController@store');
Route::get('contatos', 'UsuarioController@contatos');
Route::get('contatos_pendentes', 'UsuarioController@contatos_aceitar');
Route::post('aprovar_contato', 'UsuarioController@aprovar_contato');
Route::post('solicitar_contato', 'UsuarioController@solicitar_contato');
Route::get('buscar_contatos', 'UsuarioController@buscar_contatos');
Route::get('linguas_usuario', 'UsuarioController@linguas_usuario');
Route::get('interesses_usuario', 'UsuarioController@interesses_usuario');