<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Usuario;
use App\Contato;
use App\Usuario_lingua;
use DB;

class UsuarioController extends Controller
{
    public function store(Request $request)
    {
        $usuario = Usuario::create($request->all());

        return response()->json($usuario, 200);
    }

    public function info(Request $request)
    {
        $id = $request->input('id');
        $usuario = Usuario::find($id);

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
            $contatos = DB::table('contatos')->join('usuarios', 'usuarios.id', '=', 'contatos.id_usuario_estrangeiro') ->where('id_usuario_local', $id)->where('aceito',0)->where('estrangeiroaceito','1')->get(['usuarios.*','contatos.id as idcontato']);
        }else{
            $contatos = DB::table('contatos')->join('usuarios', 'usuarios.id', '=', 'contatos.id_usuario_local') ->where('id_usuario_estrangeiro', $id)->where('aceito',0)->where('localaceito','1')->get(['usuarios.*', 'contatos.id as idcontato']);
        }
        
        return response()->json($contatos,200);
        
    }

    public function aprovar_contato(Request $request){
        $id = $request->input('id');
        $idcontato = $request->input('idcontato');
        $usuario = Usuario::find($id);
        $contato = Contato::find($idcontato);
        if($usuario->tipo == 0){
            $contato->localaceito = 1;
        }else{
            $contato->estrangeiroaceito = 1;
        }
        $contato->aceito = 1;
        $contato->save();
        return response()->json($contato,200);
        
    }

    public function solicitar_contato(Request $request){
        $id = $request->input('id');
        $id2 = $request->input('id2');
        $usuario = Usuario::find($id);
        $usuario2 = Usuario::find($id2);
        $contato = new Contato;
        if($usuario->tipo == 0){
            $contato->id_usuario_local = $id;
            $contato->id_usuario_estrangeiro = $id2;
            $contato->localaceito = 1;
            $contato->estrangeiroaceito = 0;
        }else{
            $contato->id_usuario_local = $id2;
            $contato->id_usuario_estrangeiro = $id;
            $contato->estrangeiroaceito = 1;
            $contato->localaceito = 0;
        }
        $contato->aceito = 0;
        $contato->save();
        return response()->json($contato,200);
        
    }

    public function buscar_contatos(Request $request){
        $id = $request->input('id');
        $usuario = Usuario::find($id);
        if($usuario->tipo==0){
            $usuarios = DB::table('usuarios as u')->whereRaw("score($id,u.id) > 0 and (SELECT COUNT(*) FROM contatos where id_usuario_local = $id and id_usuario_estrangeiro = u.id LIMIT 1) = 0")->orderByRaw("score($id, u.id) DESC")->get();
        }else{
            $usuarios = DB::table('usuarios as u')->whereRaw("score($id,u.id) > 0 and (SELECT COUNT(*) FROM contatos where id_usuario_estrangeiro = $id and id_usuario_local = u.id LIMIT 1) = 0")->orderByRaw("score($id, u.id) DESC")->get();
        }
        
        return response()->json($usuarios,200);
    }

    public function linguas_usuario(Request $request){
        $id = $request->input('id');
        $linguas = DB::table('usuario_linguas as ul')->join('linguas as l', 'l.id', '=', 'ul.id_lingua')->where("id_usuario","$id")->get(['l.*']);
        return response()->json($linguas,200);
    }

    public function interesses_usuario(Request $request){
        $id = $request->input('id');
        $linguas = DB::table('usuario_interesses as ui')->join('interesses as i', 'i.id', '=', 'ui.id_interesse')->where("id_usuario","$id")->get(['i.*']);
        return response()->json($linguas,200);
    }

    public function nota_usuario(Request $request){
        $id = $request->input('id');
        $usuario = Usuario::find($id);
        if($usuario->tipo==0){
            $nota = DB::select("SELECT SUM(notalocal) / (SELECT count(*) FROM contatos where id_usuario_local = $id AND notalocal is not NULL) FROM `contatos` WHERE id_usuario_local = $id");
        }else{
            $nota = DB::select("SELECT SUM(notalocal) / (SELECT count(*) FROM contatos where id_usuario_estrangeiro = $id AND notaestrangeiro is not NULL) FROM `contatos` WHERE id_usuario_estrangeiro = $id");
        }
        
        foreach ($nota as $k => $valor){
            foreach($valor as $kk => $v){
                return response()->json((float)$v,200);
            }
            
        }
        
    }

    public function inserir_nota(Request $request){
        $id = $request->input('id');
        $id2 = $request->input('id2');
        $nota = $request->input('nota');
        $usuario = Usuario::find($id);
        
        if($usuario->tipo==0){
            
            $contato = Contato::where('id_usuario_local', $id)->where('id_usuario_estrangeiro',$id2)->first();
            $contato = Contato::find($contato->id);
            $contato->notaestrangeiro = $nota;
        }else{
            
            $contato = Contato::where('id_usuario_local', $id2)->where('id_usuario_estrangeiro',$id)->first();
            $contato = Contato::find($contato->id);
            $contato->notalocal = $nota;
        }
        $contato->save();
    
        return response()->json($contato,200);
    }

    public function contato(Request $request){
        $id = $request->input('id');
        $id2 = $request->input('id2');
        $usuario = Usuario::find($id);
        if($usuario->tipo==0){
            $contato = Contato::where('id_usuario_local', $id)->where('id_usuario_estrangeiro',$id2)->first();
            
        }else{
            $contato = Contato::where('id_usuario_local', $id2)->where('id_usuario_estrangeiro',$id)->first();
        }
        $contato->save();
    
        return response()->json($contato,200);
    }
}
