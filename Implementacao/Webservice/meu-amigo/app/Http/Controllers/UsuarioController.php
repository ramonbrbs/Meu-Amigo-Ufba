<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Usuario;
use DB;

class UsuarioController extends Controller
{
    public function store(Request $request)
    {
        $usuario = Usuario::create($request->all());

        return response()->json($usuario, 200);
    }

    public function login(Request $request){
        $email = $request->input('email');
        $senha = $request->input('senha');
        $usuario = DB::table('usuarios')->where('email', $email)->where('senha',$senha)->first();
        if(!empty($usuario)){
            return response()->json($usuario,200);
        }else{
            return response()->json("",404);
        }
    }

    public function contatos(Request $request){
        $id = $request->input('id');
        $usuario = DB::table('usuarios')->where('id', $id)->first();
        if($usuario->tipo == 0){
            $contatos = DB::table('contatos')->join('usuarios', 'usuarios.id', '=', 'contatos.id_usuario_estrangeiro') ->where('id_usuario_local', $id)->where('aceito',1)->get(['usuarios.*']);
        }else{
            $contatos = DB::table('contatos')->join('usuarios', 'usuarios.id', '=', 'contatos.id_usuario_local') ->where('id_usuario_estrangeiro', $id)->where('aceito',1)->get(['usuarios.*']);
        }
        
        return response()->json($contatos,200);
        
    }

    public function contatos_aceitar(Request $request){
        $id = $request->input('id');
        $usuario = DB::table('usuarios')->where('id', $id)->first();
        if($usuario->tipo == 0){
            $contatos = DB::table('contatos')->join('usuarios', 'usuarios.id', '=', 'contatos.id_usuario_estrangeiro') ->where('id_usuario_local', $id)->where('aceito',0)->where('estrangeiroaceito','0')->get(['usuarios.*']);
        }else{
            $contatos = DB::table('contatos')->join('usuarios', 'usuarios.id', '=', 'contatos.id_usuario_local') ->where('id_usuario_estrangeiro', $id)->where('aceito',0)->where('localaceito','0')->get(['usuarios.*']);
        }
        
        return response()->json($contatos,200);
        
    }
}
